import { Component, OnInit, ViewEncapsulation, ViewChild,ElementRef, AfterViewChecked } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";
import { PerfectScrollbarComponent } from 'ngx-perfect-scrollbar';
import { CoreService } from '../../service/core/core.service';

@Component({
   selector: 'ms-chat',
   templateUrl:'./chat-component.html',
   styleUrls: ['./chat-component.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
  animations: [ fadeInAnimation ]
})

export class ChatComponent implements OnInit, AfterViewChecked {

   selectedChat   : any;
   newMessage     : string;
   chats          : any;
   typing         : boolean = false;
   @ViewChild('chatnavbar') chatSidebar;

   randomMessages : any[] = [
      "How are you?",
      "We are glad to know",
      "How can I help you?",
      "We are happy to help you"
   ]

   @ViewChild('chatScroll')  public directiveScroll: PerfectScrollbarComponent;

   constructor(private pageTitleService: PageTitleService,
               private service : CoreService) {

   }

   ngOnInit() {
      this.pageTitleService.setTitle("Chat");
       this.service.getChatContent().
         subscribe(res => {this.chats = res},
                   err => console.log(err),
                   () =>  this.selectedChat = this.chats[0]
         );
   }     

   /**
     * isOver method is used to open the chat side nav bar 
     * according to window width
     */
   isOver(): boolean {
      return window.matchMedia(`(max-width: 960px)`).matches;
   }

   /**
     * onSelect method is used to select the paticular chart.
     */
   onSelect(chat): void {
      this.selectedChat = chat;
   }

   /**
     * send method is used to send a new message. 
     */
   send() {
      if (this.newMessage) {
         this.selectedChat.messages.push({
            msg: this.newMessage,
            from: 'me',
            time: new Date().getHours() + ':' + new Date().getMinutes() + ':'+  new Date().getSeconds()
         });
         this.newMessage = ''; 
         setTimeout(() => {
            this.typing = true;
            this.getReply();
         }, 3000);    
      }
   }

   /**
     * getReply function is used to Reply a message to admin
     */
   getReply(){
      var message = this.randomMessages[Math.floor(Math.random() * this.randomMessages.length)];
      let reply = {
         msg: message,
         from: 'them',
         time: new Date().getHours() + ':' + new Date().getMinutes() + ':'+  new Date().getSeconds()
      };
      setTimeout(() => {
         this.typing = false;
         setTimeout(() => {
            this.selectedChat.messages.push(reply);
         }, 200);
      }, 3000);
   }

   /**
     * clearMessage method is to clear the chat.
     */
   clearMessages() {
      this.selectedChat.messages.length = 0;
   }	

   ngAfterViewChecked(){
      if(this.directiveScroll){
         this.directiveScroll.directiveRef.scrollToBottom();
      } 
   }

   chatSideBarToggle(){
      this.chatSidebar.toggle();
      document.getElementById('chat-sidebar-container').classList.toggle('chat-sidebar-close');
   }
}




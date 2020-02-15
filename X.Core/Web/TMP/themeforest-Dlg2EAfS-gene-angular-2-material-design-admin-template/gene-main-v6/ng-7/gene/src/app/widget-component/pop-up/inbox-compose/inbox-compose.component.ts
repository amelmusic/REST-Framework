import { Component, OnInit,ViewEncapsulation  } from '@angular/core';
import { MatDialogRef } from "@angular/material";

@Component({
  selector: 'ms-inbox-compose',
  templateUrl: './inbox-compose.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class InboxComposeComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<InboxComposeComponent>
  ) { }

  ngOnInit() {
  }

  //send method is used to send a message.
  send() {
    this.dialogRef.close('Your message has been send.');
  }

}

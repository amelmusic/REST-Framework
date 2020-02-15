import { Component, OnInit, Renderer2, ElementRef, ViewChild, AfterViewInit, Input } from '@angular/core';
import { trigger, transition, useAnimation } from '@angular/animations';

@Component({
  selector: 'app-img-zoom',
  templateUrl: './img-zoom.component.html',
  styleUrls: ['./img-zoom.component.scss']
})
export class ImgZoomComponent implements OnInit {

   img; lens; result; cx; cy; container;
   hide = true;
   _triggerAnimationIn = false;
   notFirstTime = false;

   constructor(private renderer: Renderer2, private el: ElementRef) { }

   @ViewChild('img') imgElmRef: ElementRef;
   @ViewChild('result') resultElmRef: ElementRef;
   @ViewChild('container') containerElmRef: ElementRef;

   @Input() imgStyle = '';
   @Input() resultStyle = 'width:300px; height:300px';
   @Input() lensStyle = 'width:30px; height:30px';
   @Input() containerStyle = 'position: absolute';
   imgSrc;

   @Input('imgSrc') set _imgSrc(val) {
    this.imgSrc = val;
    if (this.notFirstTime === true) {
      this.renderer.setStyle(this.result, 'backgroundImage', "url('" + val + "')");
    }
    this.notFirstTime = true;
    // this.renderer.setStyle(this.result, 'backgroundImage', val);
   }

   ngOnInit() {
   }

   ngAfterViewInit() {
      this.img = this.imgElmRef.nativeElement;
      this.result = this.resultElmRef.nativeElement;
      this.container = this.containerElmRef.nativeElement;

      this.renderer.setAttribute(this.img, 'style', <string>this.imgStyle);
      this.renderer.setAttribute(this.result, 'style', <string>this.resultStyle);
      this.renderer.setAttribute(this.container, 'style', <string>this.containerStyle);
      this.imageZoom();
      this.renderer.setStyle(this.lens, 'visibility', 'hidden');
   }


   imageZoom() {
      /*create lens:*/
      this.lens = this.renderer.createElement('DIV');
      this.renderer.addClass(this.lens, 'img-zoom-lens');
      this.renderer.setAttribute(this.lens, 'style', <string>this.lensStyle);

      /*insert lens:*/
      this.renderer.insertBefore(this.img.parentElement, this.lens, this.img);

      /*calculate the ratio between result DIV and lens:*/
      this.cx = this.result.offsetWidth / this.lens.offsetWidth;
      this.cy = this.result.offsetHeight / this.lens.offsetHeight;

      /*set background properties for the result DIV:*/
      this.renderer.setStyle(this.result, 'backgroundImage', "url('" + this.imgSrc + "')");
      this.renderer.setStyle(this.result, 'backgroundSize', (this.img.width * this.cx) + 'px ' + (this.img.height * this.cy) + 'px');
      // this.renderer.setStyle(this.img.parentElement, 'position', 'relative')
      /*execute a function when someone moves the cursor over the image, or the lens:*/
      this.renderer.listen(this.lens, 'mousemove', this.moveLens.bind(this));
      this.renderer.listen(this.img, 'mousemove', this.moveLens.bind(this));

      /*and also for touch screens:*/
      this.renderer.listen(this.img, 'touchmove', this.moveLens.bind(this));
      this.renderer.listen(this.lens, 'touchmove', this.moveLens.bind(this));
   }

   moveLens(e) {
      let pos, x, y;
      /*prevent any other actions that may occur when moving over the image:*/
      e.preventDefault();
      /*get the cursor's x and y positions:*/
      pos = this.getCursorPos(e);
      /*calculate the position of the lens:*/
      x = pos.x - (this.lens.offsetWidth / 2);
      y = pos.y - (this.lens.offsetHeight / 2);


      /*prevent the lens from being positioned outside the image:*/
      if (x > this.img.width - this.lens.offsetWidth) {
        x = this.img.width - this.lens.offsetWidth;

        this.hide = true;
        this.renderer.setStyle(this.lens, 'visibility', 'hidden');
      } else {
        this.hide = false;
        this.renderer.setStyle(this.lens, 'visibility', 'visible');
      }

      if (x < 0) {
        x = 0;
        this.hide = true;
        this.renderer.setStyle(this.lens, 'visibility', 'hidden');
      }

      if (y > this.img.height - this.lens.offsetHeight) {
        y = this.img.height - this.lens.offsetHeight;
        this.hide = true;
        this.renderer.setStyle(this.lens, 'visibility', 'hidden');
      }

      if (y < 0) {
        y = 0;
        this.hide = true;
        this.renderer.setStyle(this.lens, 'visibility', 'hidden');
      }

      /*set the position of the lens:*/
      this.renderer.setStyle(this.lens, 'left', x + 'px');
      this.renderer.setStyle(this.lens, 'top', y + 'px');
      /*display what the lens 'sees':*/
      this.renderer.setStyle(this.result, 'backgroundPosition', '-' + (x * this.cx) + 'px -' + (y * this.cy) + 'px');
   }

   getCursorPos(e) {
      let a, x = 0, y = 0;
      e = e || window.event;
      /*get the x and y positions of the image:*/
      a = this.img.getBoundingClientRect();
      /*calculate the cursor's x and y coordinates, relative to the image:*/
      x = e.pageX - a.left;
      y = e.pageY - a.top;
      /*consider any page scrolling:*/
      x = x - window.pageXOffset;
      y = y - window.pageYOffset;
      return {x : x, y : y};
   }

}

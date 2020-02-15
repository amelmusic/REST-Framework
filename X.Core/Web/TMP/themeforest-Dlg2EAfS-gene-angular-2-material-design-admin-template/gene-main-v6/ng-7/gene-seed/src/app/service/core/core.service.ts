import { Injectable } from '@angular/core';
import 'rxjs/Rx';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

@Injectable({
	providedIn: 'root'
})

export class CoreService {
	
	sidenavOpen : boolean = true;
	sidenavMode : string = "side";
	horizontalStatus : boolean = false;
	horizontalSizeStatue : boolean =  false;
	collapseSidebar : boolean = false;

	constructor(private http : HttpClient){
	}

}
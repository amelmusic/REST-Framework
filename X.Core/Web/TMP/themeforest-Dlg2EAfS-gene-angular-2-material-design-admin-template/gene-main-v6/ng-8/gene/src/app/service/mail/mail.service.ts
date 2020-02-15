import { Injectable } from '@angular/core';
import { Mail} from "../../inbox/inbox/mail.model";
import { demoMails} from "../../inbox/inbox/mails.demo";

@Injectable()
export class MailService {

  getMessages(): Promise<Mail[]> {
    return Promise.resolve(demoMails);
  }
}

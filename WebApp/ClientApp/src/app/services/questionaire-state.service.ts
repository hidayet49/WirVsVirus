import { Injectable } from '@angular/core';
import { Questionaire } from '../models/questionaire';

@Injectable({
  providedIn: 'root'
})
export class QuestionaireStateService {

  public questionaire: Questionaire;

  constructor() { }

}

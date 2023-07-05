import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

constructor(private readonly client : HttpClient)
{}

private readonly URL =  "http://localhost:7233/api/Users/register";

AddNewRegister(Register:any){
  return this.client.post(this.URL, Register);
}
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/enviroments/enviroments';
import { ApiPaths } from '../Enums/api-paths';

@Injectable({
  providedIn: 'root'
})
export class CreatemapService {

constructor(private readonly http:HttpClient) { }
CreateMap(Map:any)
{
  return this.http.post(environment.baseUrl+ApiPaths.createmap,Map);
}
}

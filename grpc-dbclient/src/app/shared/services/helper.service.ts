import { Injectable } from '@angular/core';

@Injectable()
export class HelperService{

    fromArrayToObject(data:any[], colNames:string[]){
        let result = {};
        for(let i=0;i<colNames.length;i++){
            result[colNames[i]] = data[i];
        }
        return result;
    }
}
import { Byte } from "@angular/compiler/src/util"
import { Doctor } from "./doctor"
export class Record {
    constructor(
                public did:number, 
                public pid:number, 
                public file_description: string, 
                public attached_files :Byte[], 
                public date :Date, 
                public summary:string,
                public didNavigation:Doctor
                ){}

}


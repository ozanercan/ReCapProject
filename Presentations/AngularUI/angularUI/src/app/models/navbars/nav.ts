import { INav } from "./INav";

export class Nav implements INav{
    title: string;
    route: string;

    constructor(title:string, route:string){
        this.title=title;
        this.route=route;
    }
}
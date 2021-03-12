import { IDropDownNav } from './IDropDownNav';
import { INav } from './INav';

export class DropDownNav implements IDropDownNav {
  title: string;
  route: string;
  childNavs: INav[];

  constructor(title: string, route: string, childNavs: INav[]) {
    this.title = title;
    this.route = route;
    this.childNavs = childNavs;
  }
}

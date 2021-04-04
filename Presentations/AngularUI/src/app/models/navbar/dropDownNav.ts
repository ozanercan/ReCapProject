import { Nav } from './nav';

export class DropDownNav {
  title!: string;
  onlyAuthentication!: boolean;
  childNavs!: Nav[];
}

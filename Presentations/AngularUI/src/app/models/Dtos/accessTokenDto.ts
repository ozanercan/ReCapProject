import { User } from "./user";

export class AccessTokenDto {
  token!: string;
  expiration!: Date;
  claims!: string[];
  user!: User;
}

export class CustomerCreditCardAddDto {
  userId!: number;
  cardOwnerFullName!: string;
  cardNumber!: string;
  expiryDate!: string;
  cvv!: string;
}

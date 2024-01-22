import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "afterDiscountPrice",
  standalone: true,
})
export class AfterDiscountPricePipe implements PipeTransform {
  transform(value: number, discount: number): number {
    let discountAmount: number = value * discount;

    return value - discountAmount;
  }
}

import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class SearchService {
  private searchTextChangedSource = new Subject<string>();
  searchTextChanged$ = this.searchTextChangedSource.asObservable();

  emitSearchTextChanged(searchText: string) {
    this.searchTextChangedSource.next(searchText);
  }
}

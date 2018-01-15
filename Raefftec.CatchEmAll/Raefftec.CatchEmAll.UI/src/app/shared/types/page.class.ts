export interface Page<T> {
    readonly currentPage: number;
    readonly pageSize: number;
    readonly totalItemCount: number;
    readonly items: T[];
}

export class PaginationHelper<T> implements Page<T> {

    public get currentPage(): number {
        return this.source ? this.source.currentPage : 1;
    }

    public get pageSize(): number {
        return this.source ? this.source.pageSize : 10;
    }

    public get totalItemCount(): number {
        return this.source ? this.source.totalItemCount : 0;
    }

    public get items(): T[] {
        return this.source ? this.source.items : [];
    }

    public get pageCount(): number {
        return Math.max(Math.ceil(this.totalItemCount / this.pageSize), 1);
    }

    public get hasNext(): boolean {
        return this.currentPage < this.pageCount;
    }

    public get nextPage(): number {
        return this.currentPage + 1;
    }

    public get hasPrevious(): boolean {
        return this.currentPage > 1;
    }

    public get previousPage(): number {
        return this.currentPage - 1;
    }

    constructor(private source?: Page<T>) { }
}
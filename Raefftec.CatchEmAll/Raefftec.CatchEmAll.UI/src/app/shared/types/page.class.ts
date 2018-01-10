export class Page<T> {
    public currentPage: number;
    public pageSize: number;
    public totalItemCount: number;
    public items: T[];

    public get pageCount(): number {
        return Math.max(Math.ceil(this.totalItemCount / this.pageSize), 1);
    }

    public get hasNext(): boolean {
        return this.currentPage < this.pageCount;
    }

    public get hasPrevious(): boolean {
        return this.currentPage > 1;
    }
}
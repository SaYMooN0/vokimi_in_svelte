export class TestStatisticsTestTakenRecordsCount {
    readonly all: number;
    readonly byFollowers: number;
    readonly byFriends: number;
    readonly lastHour: number;
    readonly lastDay: number;
    readonly lastMonth: number;
    readonly lastYear: number;
    //dynamic of the records
    constructor(
        all: number,
        byFollowers: number,
        byFriends: number,
        lastHour: number,
        lastDay: number,
        lastMonth: number,
        lastYear: number
    ) {
        this.all = all;
        this.byFollowers = byFollowers;
        this.byFriends = byFriends;
        this.lastHour = lastHour;
        this.lastDay = lastDay;
        this.lastMonth = lastMonth;
        this.lastYear = lastYear;
    }
}
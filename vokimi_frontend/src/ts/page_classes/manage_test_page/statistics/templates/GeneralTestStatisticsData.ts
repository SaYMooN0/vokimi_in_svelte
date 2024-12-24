import type { GeneralTestResultStatisticsData } from "./general/GeneralTestResultStatisticsData";


export class GeneralTestStatisticsData {
    resultsData: GeneralTestResultStatisticsData[];
    constructor(resultsData: GeneralTestResultStatisticsData[]) {
        this.resultsData = resultsData;
    }
}

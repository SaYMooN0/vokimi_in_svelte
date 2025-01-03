export class GeneralTestTakenReceivedResultData {
    receivedResultName: string;
    receivedResultImage: string;
    receivedResultText: string;
    allResult: GeneralTestTakenResultVm[];
    constructor(
        receivedResultName: string,
        receivedResultImage: string,
        ReceivedResultText: string,
        allResult: GeneralTestTakenResultVm[]
    ) {
        this.receivedResultName = receivedResultName;
        this.receivedResultImage = receivedResultImage;
        this.receivedResultText = ReceivedResultText;
        this.allResult = allResult;
    }

}
export class GeneralTestTakenResultVm {
    id: string;
    name: string;
    image: string;
    receivingPercentage: number;
    constructor(
        id: string,
        name: string,
        image: string,
        receivingPercentage: number
    ) {
        this.id = id;
        this.name = name;
        this.image = image;
        this.receivingPercentage = receivingPercentage;
    }
}

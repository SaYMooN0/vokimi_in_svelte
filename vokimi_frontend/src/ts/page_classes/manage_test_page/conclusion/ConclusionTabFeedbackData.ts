import { FeedbackRecordData } from "./FeedbackRecordData";

export class ConclusionTabFeedbackData {
    accompanyingText: string;
    maxLength: number;

    constructor(
        accompanyingText: string,
        maxLength: number,
    ) {
        this.accompanyingText = accompanyingText;
        this.maxLength = maxLength;
    }
}



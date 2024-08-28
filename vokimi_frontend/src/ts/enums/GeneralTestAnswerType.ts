export enum GeneralTestAnswerType {
    TextOnly = "TextOnly",
    TextAndImage = "TextAndImage",
    ImageOnly = "ImageOnly"
}

export namespace GeneralTestAnswerType {
    const idToEnumMap: { [key: string]: GeneralTestAnswerType } = {
        "text_only": GeneralTestAnswerType.TextOnly,
        "text_and_image": GeneralTestAnswerType.TextAndImage,
        "image_only": GeneralTestAnswerType.ImageOnly,
    };

    export function fromId(id: string): GeneralTestAnswerType {
        return idToEnumMap[id] || GeneralTestAnswerType.TextOnly;
    }
}

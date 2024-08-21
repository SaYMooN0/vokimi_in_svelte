export enum TestTemplate {
    General = "General",
    Knowledge = "Knowledge"
}

export function getFeatures(template: TestTemplate): string[] {
    switch (template) {
        case TestTemplate.General:
            return [
                "Completely customize your test the way you want it",
                "No restrictions or necessities (almost)"
            ];
        case TestTemplate.Knowledge:
            return [
                "See how well test takers know the subject",
                "Specially selected types of questions and the method of evaluating answers"
            ];
        default:
            throw new Error("Not implemented");
    }
}

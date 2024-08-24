export enum TestTemplate {
    General = "General",
    Scoring = "Scoring"
}

export function getFeatures(template: TestTemplate): string[] {
    switch (template) {
        case TestTemplate.General:
            return [
                "Basic template that let's create almost any kind of test",
                "A simple system where the answer to a question leads to the specific result(s)"
            ];
        case TestTemplate.Scoring:
            return [
                "Let test takers see how well they ",
                "Specially made system for scoring points and getting a result based on them"
            ];
        default:
            throw new Error("Not implemented");
    }
}

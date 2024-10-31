<script lang="ts">
    import type { TestTakingConclusionData } from "../../../../ts/page_classes/test_taking_page/ITestTakingData";

    export let conclusionData: TestTakingConclusionData;
    export function getFeedback(): string | null {
        if (!conclusionData.anyFeedback) {
            return null;
        }
        return feedback;
    }
    let feedback: string = "";
</script>

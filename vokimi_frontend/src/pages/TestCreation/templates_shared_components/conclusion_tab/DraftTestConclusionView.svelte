<script lang="ts">
    import { TestCreationConclusionTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import TextWithOptionalImageInput from "../../creation_shared_components/TextWithOptionalImageInput.svelte";

    export let conclusionData: TestCreationConclusionTabData;
    export let testId: string;

    async function loadData() {
        const url = "/api/testCreation/getDraftTestConclusionData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            conclusionData = new TestCreationConclusionTabData(
                data.id,
                data.text,
                data.additionalImage,
                data.anyFeedback,
                data.feedbackText,
                data.maxFeedbackLength,
            );
        } else {
            conclusionData = TestCreationConclusionTabData.empty();
        }
    }
    let dataFetchingErr: string;
</script>

<TabViewDataLoader {loadData} isEmpty={() => conclusionData.isEmpty()}>
    <div slot="empty">
        <h2>It seems like you haven't added conclusion yet</h2>
        <button>Add conclusion</button>
        <p class="error-message">{dataFetchingErr}</p>
    </div>
    <div slot="content">
        <TextWithOptionalImageInput />
        <p>{JSON.stringify(conclusionData)}</p>
    </div>
</TabViewDataLoader>

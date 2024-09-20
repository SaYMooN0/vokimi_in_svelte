<script lang="ts">
    import type { TestCreationConclusionTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import TextWithOptionalImageInput from "../../creation_shared_components/TextWithOptionalImageInput.svelte";

    export let conclusionData: TestCreationConclusionTabData;
    export let testId: string;

    async function loadData() {
        const url = "/api/testCreation/getTestConclusionData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
        } else {
            //  = .empty();
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
    </div>
</TabViewDataLoader>

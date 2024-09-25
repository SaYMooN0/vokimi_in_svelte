<script lang="ts">
    import { TestCreationTagsTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import TagsEditingDialog from "./TagsEditingDialog.svelte";

    export let tagsData: TestCreationTagsTabData;
    export let testId: string;
    let fetchedCorrectly: boolean = false;
    let tagsEditingDialog: TagsEditingDialog;

    async function loadData() {
        const url = "/api/testCreation/getDraftTestTagsData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            tagsData = new TestCreationTagsTabData(
                data.tags,
                data.maxTagsForTestCount,
                data.maxTagNameLength,
            );
            fetchedCorrectly = true;
        } else {
            tagsData = TestCreationTagsTabData.empty();
            fetchedCorrectly = false;
        }
    }
</script>

<TabViewDataLoader {loadData} isEmpty={() => fetchedCorrectly}>
    <div slot="empty" class="no-conclusion-div">
        <h2>Unable to fetch data. Please try again later.</h2>
    </div>
    <div slot="content" class="conclusion-data">
        <TagsEditingDialog
            bind:this={tagsEditingDialog}
            updateParentElementData={loadData}
        />
        <TabHeaderWithButton
            tabName="Test Tags ({tagsData.tags.length === 0
                ? 'No tags added yet'
                : tagsData.tags.length + 1}):"
            buttonText="Open Tags Editor"
            onButtonClick={() => tagsEditingDialog.open(tagsData)}
        />
    </div>
</TabViewDataLoader>

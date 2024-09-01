<script lang="ts">
    import { TestCreationMainInfoTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    import LoadingMessage from "../../../../components/shared/LoadingMessage.svelte";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { LanguageUtils } from "../../../../ts/enums/Language";
    import { TestPrivacyUtils } from "../../../../ts/enums/TestPrivacy";
    import MainInfoViewLeftPart from "./MainInfoViewLeftPart.svelte";
    import MainInfoViewRightPart from "./MainInfoViewRightPart.svelte";
    import ErrorMessageInCenter from "../../creation_shared_components/ErrorMessageInCenter.svelte";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftTestMainInfoEditingDialog from "./DraftTestMainInfoEditingDialog.svelte";

    export let mainInfoData: TestCreationMainInfoTabData;
    export let testId: string;

    async function loadTabData() {
        const url = "/api/test-creation/getDraftTestMainInfoData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            mainInfoData.update(
                TestTemplateUtils.fromId(data.template),
                data.name,
                LanguageUtils.fromId(data.language),
                TestPrivacyUtils.fromId(data.privacy),
                data.description,
                data.imgPath,
            );
        } else {
            mainInfoData = TestCreationMainInfoTabData.empty();
        }
    }
    async function onTabEnter() {
        if (mainInfoData.isEmpty()) {
            await loadTabData();
        }
    }
    function openEditingDialog() {
        mainInfoEditingDialog.open(
            mainInfoData.name,
            mainInfoData.description,
            mainInfoData.language,
            mainInfoData.privacy,
        );
    }
    let mainInfoEditingDialog: DraftTestMainInfoEditingDialog;
</script>

{#await onTabEnter()}
    <LoadingMessage />
{:then}
    {#if mainInfoData.isEmpty()}
        <ErrorMessageInCenter
            errorMessage="An error has occurred. Please refresh the page"
        />
    {:else}
        <DraftTestMainInfoEditingDialog
            bind:this={mainInfoEditingDialog}
            updateParentElementData={loadTabData}
            {testId}
        />
        <TabHeaderWithButton
            tabName="Main info"
            buttonText="Edit"
            onButtonClick={() => openEditingDialog()}
        />
        <div class="tab-content">
            <div class="left-div">
                <MainInfoViewLeftPart
                    template={mainInfoData.template}
                    testName={mainInfoData.name}
                    description={mainInfoData.description}
                    language={mainInfoData.language}
                    privacy={mainInfoData.privacy}
                />
            </div>
            <div class="right-div">
                <MainInfoViewRightPart imgPath={mainInfoData.imgPath} />
            </div>
        </div>
    {/if}
{/await}

<style>
    .tab-content {
        display: grid;
        grid-template-columns: 1fr auto;
        height: max-content;
    }
</style>

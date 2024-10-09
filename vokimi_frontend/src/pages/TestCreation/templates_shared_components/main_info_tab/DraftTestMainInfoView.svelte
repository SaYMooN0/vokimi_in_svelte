<script lang="ts">
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { LanguageUtils } from "../../../../ts/enums/Language";
    import { PrivacyValuesUtils } from "../../../../ts/enums/PrivacyValues";
    import MainInfoViewLeftPart from "./MainInfoViewLeftPart.svelte";
    import MainInfoViewRightPart from "./MainInfoViewRightPart.svelte";
    import ErrorMessageInCenter from "../../creation_shared_components/ErrorMessageInCenter.svelte";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import DraftTestMainInfoEditingDialog from "./DraftTestMainInfoEditingDialog.svelte";
    import { TestCreationMainInfoTabData } from "../../../../ts/my_tests_page/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";

    export let mainInfoData: TestCreationMainInfoTabData;
    export let testId: string;
    export let updateTestName: (name: string) => void;

    async function loadData() {
        const url = "/api/testCreation/getDraftTestMainInfoData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            mainInfoData = new TestCreationMainInfoTabData(
                TestTemplateUtils.fromId(data.template),
                data.name,
                LanguageUtils.fromId(data.language),
                PrivacyValuesUtils.fromId(data.privacy),
                data.description,
                data.imgPath,
            );
            updateTestName(mainInfoData.name);
        } else {
            mainInfoData = TestCreationMainInfoTabData.empty();
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

<TabViewDataLoader {loadData} isEmpty={() => mainInfoData.isEmpty()}>
    <ErrorMessageInCenter
        slot="empty"
        errorMessage="Unable to fetch data. Please try again later."
    />
    <div slot="content">
        <DraftTestMainInfoEditingDialog
            bind:this={mainInfoEditingDialog}
            updateParentElementData={loadData}
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
                <MainInfoViewRightPart
                    draftTestId={testId}
                    bind:imgPath={mainInfoData.imgPath}
                />
            </div>
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
        display: grid;
        grid-template-columns: 1fr auto;
        height: max-content;
    }
</style>

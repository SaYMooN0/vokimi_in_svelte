<script lang="ts">
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import ErrorMessageInCenter from "../../creation_shared_components/ErrorMessageInCenter.svelte";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import { TestCreationSettingsTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationSettingsTabData";
    import TestSettingsEditingDialog from "../../../../components/shared/dialogs/TestSettingsEditingDialog.svelte";
    import { Err } from "../../../../ts/Err";
    import { PrivacyValuesUtils } from "../../../../ts/enums/PrivacyValues";
    import YesNoIconDisplay from "./YesNoIconDisplay.svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";

    export let settingsData: TestCreationSettingsTabData;
    export let testId: string;

    let isFetchedCorrectly: boolean = false;
    async function loadData() {
        isFetchedCorrectly = false;
        const url = "/api/testCreation/getDraftTestSettingsData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            console.log(data);

            settingsData = new TestCreationSettingsTabData(
                PrivacyValuesUtils.fromId(data.privacy),
                data.discussionsOpen,
                data.testTakenPostsAllowed,
                data.enableTestRatings,
            );
            isFetchedCorrectly = true;
        } else {
            isFetchedCorrectly = false;
        }
    }

    function openEditingDialog() {
        settingsEditingDialog.open(
            settingsData.privacy,
            settingsData.discussionsOpen,
            settingsData.testTakenPostsAllowed,
            settingsData.enableTestRatings,
        );
    }
    async function saveSettingsData(): Promise<Err> {
        const data = { ...settingsData, testId };
        const response = await fetch(
            "/api/testCreation/updateDraftTestSettings",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            },
        );

        if (response.ok) {
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred. Please try again.");
        }
    }

    let settingsEditingDialog: TestSettingsEditingDialog;
</script>

<TabViewDataLoader
    {loadData}
    isEmpty={() => {
        return !isFetchedCorrectly;
    }}
>
    <div slot="content">
        <TestSettingsEditingDialog
            bind:this={settingsEditingDialog}
            saveTestSettings={saveSettingsData}
        />
        <TabHeaderWithButton
            tabName="Settings"
            buttonText="Edit"
            onButtonClick={() => openEditingDialog()}
        />
        <div class="tab-content">
            <p class="prop-name-val-p">
                Privacy:
                <span class="prop-value">
                    {PrivacyValuesUtils.getFullName(settingsData.privacy)}
                </span>
            </p>
            <p class="prop-name-val-p">
                Enable test ratings:
                <YesNoIconDisplay value={settingsData.enableTestRatings} />
            </p>
            <p class="prop-name-val-p">
                Discussions open:
                <YesNoIconDisplay value={settingsData.discussionsOpen} />
            </p>
            <p class="prop-name-val-p">
                Allow users to create posts when about their test takings:
                <YesNoIconDisplay value={settingsData.testTakenPostsAllowed} />
            </p>
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
        direction: flex;
        flex-direction: column;
        margin-top: 12px;
    }
    .prop-name-val-p {
        display: flex;
        flex-direction: row;
        align-items: center;
        margin: 8px 12px;
        gap: 12px;
        font-size: 20px;
    }
    .prop-name-val-p .prop-value {
        font-weight: 500;
    }
</style>

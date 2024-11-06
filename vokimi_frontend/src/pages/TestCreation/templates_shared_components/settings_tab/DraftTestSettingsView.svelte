<script lang="ts">
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import ErrorMessageInCenter from "../../creation_shared_components/ErrorMessageInCenter.svelte";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import type { TestCreationSettingsTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationSettingsTabData";
    import TestSettingsEditingDialog from "../../../../components/shared/dialogs/TestSettingsEditingDialog.svelte";
    import { Err } from "../../../../ts/Err";
    import {
        PrivacyValues,
        PrivacyValuesUtils,
    } from "../../../../ts/enums/PrivacyValues";
    import { get } from "svelte/store";
    import YesNoIconDisplay from "./YesNoIconDisplay.svelte";

    export let settingsData: TestCreationSettingsTabData;
    export let testId: string;

    async function loadData() {}

    function openEditingDialog() {
        settingsEditingDialog.open(
            settingsData.privacy,
            settingsData.discussionsOpen,
            settingsData.testTakenPostsAllowed,
            settingsData.enableTestRatings,
        );
    }
    async function saveSettingsData(): Promise<Err> {
        return new Err("Not implemented");
    }

    let settingsEditingDialog: TestSettingsEditingDialog;
</script>

<TabViewDataLoader
    {loadData}
    isEmpty={() => {
        return false;
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

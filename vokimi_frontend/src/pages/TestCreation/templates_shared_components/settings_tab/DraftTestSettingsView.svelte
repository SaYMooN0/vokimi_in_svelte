<script lang="ts">
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import ErrorMessageInCenter from "../../creation_shared_components/ErrorMessageInCenter.svelte";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import type { TestCreationSettingsTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationSettingsTabData";
    import TestSettingsEditingDialog from "../../../../components/shared/dialogs/TestSettingsEditingDialog.svelte";
    import { Err } from "../../../../ts/Err";

    export let settingsData: TestCreationSettingsTabData;
    export let testId: string;

    async function loadData() {}

    function openEditingDialog() {
        settingsEditingDialog.open(
            settingsData.privacy,
            settingsData.discussionsOpen,
            settingsData.testTakenPostsAllowed,
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
            <p>Settings</p>
            <p>Privacy: {settingsData.privacy}</p>
            <p>Discussions open: {settingsData.discussionsOpen}</p>
            <p>
                Allow users to create posts when about their test takings: {settingsData.testTakenPostsAllowed}
            </p>
        </div>
    </div>
</TabViewDataLoader>

<style>
    .tab-content {
        direction: flex;
        flex-direction: column;
    }
</style>

<script lang="ts">
    import {
        PrivacyValues,
        PrivacyValuesUtils,
    } from "../../../ts/enums/PrivacyValues";
    import type { Err } from "../../../ts/Err";
    import BaseDialog from "../../BaseDialog.svelte";
    import CloseButton from "../CloseButton.svelte";
    import CustomSwitch from "../CustomSwitch.svelte";

    export let updateParentElementData: () => void;
    export let saveTestSettings: (
        privacy: PrivacyValues,
        discussionsOpen: boolean,
        testTakenPostsAllowed: boolean,
        enableTestRatings: boolean,
    ) => Promise<Err>;
    let dialogElement: BaseDialog;
    let privacy: PrivacyValues;
    let discussionsOpen: boolean;
    let testTakenPostsAllowed: boolean;
    let enableTestRatings: boolean;
    export function open(
        privacyVal: PrivacyValues,
        discussionsOpenVal: boolean,
        testTakenPostsAllowedVal: boolean,
        enableTestRatingsVal: boolean,
    ) {
        errorMessage = "";
        privacy = privacyVal;
        discussionsOpen = discussionsOpenVal;
        testTakenPostsAllowed = testTakenPostsAllowedVal;
        enableTestRatings = enableTestRatingsVal;
        dialogElement.open();
    }

    let errorMessage: string = "";
    async function saveBtnClicked() {
        let savingErr: Err = await saveTestSettings(
            privacy,
            discussionsOpen,
            testTakenPostsAllowed,
            enableTestRatings,
        );
        if (savingErr.notNone()) {
            errorMessage = savingErr.toString();
        } else {
            dialogElement.close();
            updateParentElementData();
        }
    }
</script>

<BaseDialog dialogId="test-settings-editing-dialog" bind:this={dialogElement}>
    <CloseButton onClose={() => dialogElement.close()} />
    <div class="dialog-content">
        <p class="dialog-header">Settings editing</p>
        <div class="property-vals-container">
            <p class="prop-name-val-p">
                <span>Open discussions for this test:</span>
                <CustomSwitch
                    id="settings-discussions"
                    bind:isChecked={discussionsOpen}
                />
            </p>
            <p class="prop-name-val-p">
                <span>Allow users to create posts after test is taken:</span>
                <CustomSwitch
                    id="settings-test-taken-posts"
                    bind:isChecked={testTakenPostsAllowed}
                />
            </p>
            <p class="prop-name-val-p">
                <span>Enable test ratings:</span>
                <CustomSwitch
                    id="enable-test-ratings"
                    bind:isChecked={enableTestRatings}
                />
            </p>
            <label class="prop-name-val-p" for="test-privacy">
                <span>Test privacy:</span>
                <select id="test-privacy" bind:value={privacy}>
                    {#each Object.values(PrivacyValues) as pr}
                        <option value={pr}>
                            {PrivacyValuesUtils.getFullName(pr)}
                        </option>
                    {/each}
                </select>
            </label>
        </div>
        <p class="error-message">{errorMessage}</p>
        <div class="save-btn unselectable" on:click={saveBtnClicked}>
            Save changes
        </div>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        width: min(800px, 72vw);
        display: flex;
        flex-direction: column;
        padding: 8px 32px;
    }
    .dialog-header {
        font-size: 28px;
        color: var(--text);
        margin: 12px 16px;
    }

    .property-vals-container {
        display: flex;
        flex-direction: column;
        gap: 8px;
    }
    .prop-name-val-p {
        margin: 4px 0;
        color: var(--text);
        display: flex;
        align-items: center;
        justify-content: space-between;
        background-color: var(--back-main);
        padding: 4px 12px;
        border-radius: 4px;
    }
    .prop-name-val-p span {
        font-size: 22px;
        font-weight: 500;
    }
    .save-btn {
        margin: 20px auto;
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 8px 24px;
        font-size: 18px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .save-btn:hover {
        background-color: var(--primary-hov);
    }
    .error-message {
        color: var(--red-del);
        font-size: 20px;
        margin: 4px 0;
        text-align: center;
    }
</style>

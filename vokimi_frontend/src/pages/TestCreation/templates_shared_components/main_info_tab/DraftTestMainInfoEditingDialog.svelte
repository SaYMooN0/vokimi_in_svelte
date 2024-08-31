<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import { Language, LanguageUtils } from "../../../../ts/enums/Language";
    import {
        TestPrivacy,
        TestPrivacyUtils,
    } from "../../../../ts/enums/TestPrivacy";
    import EditingDialogCloseButton from "../../creation_shared_components/EditingDialogCloseButton.svelte";

    export let updateParentElementData: () => void;

    export function open(
        testNameVal: string,
        descriptionVal: string,
        languageVal: Language,
        privacyVal: TestPrivacy,
    ) {
        testName = testNameVal;
        description = descriptionVal;
        language = languageVal;
        privacy = privacyVal;
        dialogElement.open();
    }

    let testName: string = "";
    let description: string = "";
    let language: Language;
    let privacy: TestPrivacy;

    let dialogElement: BaseDialog;
    let errorMessage: string = "";

    async function saveData() {
        console.log("before valid");

        validateFormData();
        console.log("after valid");
        if (!valid) {
            return;
        }
        // await updateParentElementData();
        dialogElement.close();
    }
    function validateFormData() {
        valid = true;
        errorMessage = "";
        if (testName === "") {
            valid = false;
            errorMessage = "Test name cannot be empty";
            return;
        }
        if (language === null) {
            valid = false;
            errorMessage = "Please select a language";
            return;
        }
        if (privacy === null) {
            valid = false;
            errorMessage = "Please select a privacy";
            return;
        }
    }

    let valid: boolean = true;
</script>

<BaseDialog dialogId="draftTestMainInfo" bind:this={dialogElement}>
    <div class="dialog-body">
        <EditingDialogCloseButton onClose={() => dialogElement.close()} />

        <label for="testName" class="property-label">Test name:</label>
        <input id="testName" type="text" bind:value={testName} />

        <label for="description" class="property-label"
            >Test description:
        </label>
        <textarea
            id="description"
            bind:value={description}
            placeholder="Test description is optional"
        />

        <label for="language" class="property-label">Language:</label>
        <select id="language" bind:value={language}>
            {#each Object.values(Language) as language}
                <option value={language}>
                    {LanguageUtils.getFullName(language)}
                </option>
            {/each}
        </select>

        <label for="privacy" class="property-label">Privacy:</label>
        <select id="privacy" bind:value={privacy}>
            {#each Object.values(TestPrivacy) as privacy}
                <option value={privacy}>
                    {TestPrivacyUtils.getFullName(privacy)}
                </option>
            {/each}
        </select>
        <button on:click={saveData}>Save</button>
    </div>
</BaseDialog>

<style>
    .dialog-body {
        position: relative;
        padding: 10px 20px;
        display: flex;
        flex-direction: column;
        gap: 20px;
    }
    .property-label {
        color: var(--text-faded);
    }
</style>

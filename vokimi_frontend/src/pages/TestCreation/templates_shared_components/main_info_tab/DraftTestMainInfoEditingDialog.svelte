<script lang="ts">
    import { Language, LanguageUtils } from "../../../../ts/enums/Language";
    import {
        PrivacyValues,
        PrivacyValuesUtils,
    } from "../../../../ts/enums/PrivacyValues";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    export function open(
        testNameVal: string,
        descriptionVal: string,
        languageVal: Language,
        privacyVal: PrivacyValues,
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
    let privacy: PrivacyValues;

    let dialogElement: BaseDraftTestEditingDialog;

    async function saveData() {
        const dataErr: string | null = checkFormDataForError();
        if (dataErr !== null) {
            dialogElement.setErrorMessage(dataErr);
            return;
        }
        const response = await fetch(
            "/api/testCreation/updateDraftTestMainInfoData",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    testId: testId,
                    name: testName.trim(),
                    description: description.trim(),
                    language: language,
                    privacy: privacy,
                }),
            },
        );
        if (response.ok) {
            await updateParentElementData();
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
    function checkFormDataForError(): string | null {
        if (testName === "") {
            return "Test name cannot be empty";
        }
        if (language === null) {
            return "Please select a language";
        }
        if (privacy === null) {
            return "Please select a privacy";
        }
        return null;
    }
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    <div class="dialog-content">
        <label for="testName" class="property-label">Test name:</label>
        <input
            id="testName"
            type="text"
            bind:value={testName}
            name="testName-{StringUtils.randomString()}"
        />

        <label for="description" class="property-label"
            >Test description:
        </label>
        <textarea
            class="test-description"
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
            {#each Object.values(PrivacyValues) as privacy}
                <option value={privacy}>
                    {PrivacyValuesUtils.getFullName(privacy)}
                </option>
            {/each}
        </select>
    </div>
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        width: min(1400px, 72vw);
        display: flex;
        flex-direction: column;
        padding: 8px 12px;
    }
    .property-label {
        color: var(--text-faded);
    }
    .test-description {
        background-color: var(--back-secondary);
        color: var(--text);
        max-height: max(12vh, 400px);
        resize: vertical;
        outline: none;
        border: 2px solid var(--back-secondary);
        border-radius: 8px;
        box-sizing: border-box;
        padding: 4px 8px;
    }
    .test-description:focus {
        border-color: var(--primary);
    }
</style>

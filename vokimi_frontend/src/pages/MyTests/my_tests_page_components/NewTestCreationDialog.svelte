<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    import { TestTemplate } from "../../../ts/enums/TestTemplate";
    import TemplateCard from "./new_test_creation_components/TemplateCard.svelte";
    import { navigate } from "svelte-routing";

    let dialogElement: BaseDialog;

    export function open() {
        dialogElement.open();
    }

    async function createNewTest() {
        if (chosenTemplate === undefined || chosenTemplate === null) {
            errorMessage = "Please choose a template";
            return;
        }
        if (chosenTemplate == TestTemplate.Knowledge) {
            alert("Not implemented yet");
            return;
        }
        const response = await fetch("/api/userTests/createNew");

        if (response.status === 200) {
            const data = await response.json();
            navigate("/my-tests/test-creation/" + data.testId);
        } else if (response.status === 400) {
            const data = await response.json();
            errorMessage = data.error || "An unknown error occurred.";
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
    const templates = Object.values(TestTemplate);
    let chosenTemplate: TestTemplate;
    let errorMessage: string = "";
</script>

<BaseDialog dialogId="new-test-creation-dialog" bind:this={dialogElement}>
    <div class="dialog-content unselectable">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            xmlns:xlink="http://www.w3.org/1999/xlink"
            class="close-btn"
            viewBox="0 0 384 512"
            on:click={dialogElement.close}
        >
            <path
                d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"
            />
        </svg>
        <label class="choose-template-label">Choose a template</label>
        <div class="templates-container">
            {#each templates as template}
                <TemplateCard
                    {template}
                    isActive={template === chosenTemplate}
                    onClick={() => (chosenTemplate = template)}
                />
            {/each}
        </div>
        <label class="server-message">{errorMessage}</label>
        <div class="create-btn" on:click={createNewTest}>Create</div>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 10px 40px;
        position: relative;
    }
    .close-btn {
        position: absolute;
        right: 12px;
        top: 12px;
        background-color: var(--text-faded);
        height: 30px;
        padding: 5px;
        box-sizing: border-box;
        aspect-ratio: 1/1;
        border-radius: 50%;
        fill: white;
        cursor: pointer;
    }

    .close-btn:hover {
        background-color: var(--red-del);
    }

    .choose-template-label {
        text-align: center;
        font-size: 36px;
        font-weight: 600;
        color: var(--primary);
        margin: 20px 0;
    }

    .templates-container {
        padding: 10px;
        gap: 40px;
        display: flex;
        justify-content: space-evenly;
    }
    .create-btn {
        margin-top: 32px;
        margin-bottom: 24px;
        padding: 8px 32px;
        font-size: 20px;
        font-weight: 600;
        letter-spacing: 1px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        background-color: var(--primary);
        color: var(--back-main);
        transition:
            background-color 0.1s,
            padding 0.18s ease-in;
    }

    .create-btn:hover {
        background-color: var(--primary-hov);
        padding: 8px 40px;

    }
</style>

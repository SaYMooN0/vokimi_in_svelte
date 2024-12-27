<script lang="ts">
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    export let testId: string;
    export let updateTabData: () => void;
    async function createConclusion() {
        const response = await fetch(
            `/api/manageTest/conclusion/createConclusionForTest/${testId}`,
            {
                method: "POST",
            },
        );
        if (response.ok) {
            conclusionCreationErr = "";
            updateTabData();
        } else if (response.status === 400) {
            conclusionCreationErr = await getErrorFromResponse(response);
        } else {
            conclusionCreationErr = "Something went wrong...";
        }
    }
    let conclusionCreationErr: string = "";
</script>

<div class="no-conclusion-div">
    <h1>This test has no conclusion</h1>
    <p>
        Enable conclusion to have a place for some words in the end of the test
        and ability to enable feedback for test takers
    </p>
    {#if !StringUtils.isNullOrWhiteSpace(conclusionCreationErr)}
        <p class="conclusion-creation-err">{conclusionCreationErr}</p>
    {/if}
    <button class="create-conclusion-btn" on:click={createConclusion}>
        Create conclusion
    </button>
</div>

<style>
    .no-conclusion-div {
        display: flex;
        flex-direction: column;
        align-items: center;
        position: absolute;
        top: 40%;
        left: 50%;
        transform: translateX(-50%);
    }
    .conclusion-creation-err {
        color: var(--red-del);
    }
    .create-conclusion-btn {
        padding: 6px 16px;
        border: none;
        color: var(--back-main);
        background-color: var(--primary);
        font-size: 18px;
        border-radius: 4px;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }
    .create-conclusion-btn:hover {
        padding: 6px 20px;
        background-color: var(--primary-hov);
    }
</style>

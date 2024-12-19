<script lang="ts">
    import CustomCheckbox from "../../../../../../../../../components/shared/CustomCheckbox.svelte";
    import { StringUtils } from "../../../../../../../../../ts/utils/StringUtils";

    let resultsCheckboxInputName: string =
        "results-" + StringUtils.randomString(4);
    export let allResults: { [key: string]: string };
    export let changeStateToResultCreation: () => void;

    export let chosenResults: { [key: string]: string } = {};

    function toggleSelection(key: string, value: string) {
        if (chosenResults[key]) {
            delete chosenResults[key];
            chosenResults = chosenResults;
        } else {
            chosenResults[key] = value;
        }
    }
</script>

<p class="result-assigning-title">Choose results from the following:</p>
<div class="results-options">
    {#if Object.keys(allResults).length === 0}
        <p class="no-results-label">There are no results to choose from</p>
    {:else}
        {#each Object.entries(allResults) as [key, value]}
            <div
                class="result-div"
                on:click={() => toggleSelection(key, value)}
            >
                <div
                    class="chosen-indicator"
                    class:res-chosen={chosenResults[key]}
                >
                    <div></div>
                </div>
                {value}
            </div>
        {/each}
    {/if}

    <div
        class="create-res-btn unselectable"
        on:click={changeStateToResultCreation}
    >
        Create New
    </div>
</div>

<style>
    .result-assigning-title {
        margin: 0;
        font-size: 24px;
        font-weight: 500;
        color: var(--text);
    }
    .results-options {
        margin-top: 10px;
        width: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 4px;
        overflow-y: auto;
        min-height: 120px;
        max-height: 60vh;
    }
    .result-div {
        width: 100%;
        display: grid;
        grid-template-columns: 20px 1fr;
        box-sizing: border-box;
        gap: 4px;
        font-size: 18px;
        color: var(--text);
        cursor: pointer;
        transition: all 0.14s ease-in;
    }
    .chosen-indicator {
        width: 100%;
        aspect-ratio: 1/1;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 2px;
        box-sizing: border-box;
        border: 2px solid var(--text-faded);
        border-radius: 4px;
    }
    .chosen-indicator div {
        width: 100%;
        height: 100%;
        background-color: transparent;
        border-radius: 2px;
    }
    .result-div:hover {
        padding-left: 4px;
        gap: 6px;
    }
    .result-div:hover .chosen-indicator {
        border-color: var(--primary);
    }
    .chosen-indicator.res-chosen {
        border-color: var(--primary);
    }
    .chosen-indicator.res-chosen div {
        background-color: var(--primary);
    }
    .create-res-btn {
        margin-top: 12px;
        background-color: var(--primary);
        padding: 4px 20px;
        color: var(--back-main);
        font-size: 20px;
        border-radius: 100px;
        cursor: pointer;
        transition: all 0.12s ease;
    }

    .create-res-btn:hover {
        padding: 4px 28px;
        background-color: var(--primary-hov);
    }

    .create-res-btn:active {
        transform: scale(0.97);
    }
</style>

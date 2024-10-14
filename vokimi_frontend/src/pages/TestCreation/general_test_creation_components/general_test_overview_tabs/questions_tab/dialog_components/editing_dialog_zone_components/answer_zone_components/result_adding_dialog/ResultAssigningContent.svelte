<script lang="ts">
    import { StringUtils } from "../../../../../../../../../ts/utils/StringUtils";

    let resultsCheckboxInputName: string =
        "results-" + StringUtils.randomString(4);
    export let allResults: { [key: string]: string };
    export let changeStateToResultCreation: () => void;

    export let chosenResults: { [key: string]: string } = {};

    function toggleSelection(key: string, value: string) {
        if (chosenResults[key]) {
            delete chosenResults[key];
        } else {
            chosenResults[key] = value;
        }
    }
</script>

<div class="result-assigning-state">
    <p class="result-assigning-title">Choose results from the following:</p>
    <div class="results-options">
        {#if Object.keys(allResults).length === 0}
            <p class="no-results-label">There are no results to choose from</p>
        {:else}
            {#each Object.entries(allResults) as [key, value]}
                <input
                    type="checkbox"
                    name={resultsCheckboxInputName}
                    id={resultsCheckboxInputName + key}
                    on:change={() => toggleSelection(key, value)}
                    checked={key in chosenResults}
                />
                <label
                    class="result-option"
                    for={resultsCheckboxInputName + key}
                >
                    {value}
                </label>
            {/each}
        {/if}

        <div
            class="create-res-btn unselectable"
            on:click={changeStateToResultCreation}
        >
            Create New
        </div>
    </div>
</div>

<style>
    .results-options {
        margin-top: 10px;
        width: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 4px;
        overflow-y: auto;
        background-color: var(--back-secondary);
        min-height: 120px;
        max-height: 60vh;
    }
    .create-res-btn {
        margin-top: auto;
        background-color: var(--primary);
        padding: 5px 20px;
        color: var(--back-main);
        font-size: 20px;
        border-radius: 100px;
        cursor: pointer;
        transition: all 0.12s ease;
    }

    .create-res-btn:hover {
        padding: 5px 28px;
        background-color: var(--primary-hov);
    }

    .create-res-btn:active {
        transform: scale(0.97);
    }
</style>

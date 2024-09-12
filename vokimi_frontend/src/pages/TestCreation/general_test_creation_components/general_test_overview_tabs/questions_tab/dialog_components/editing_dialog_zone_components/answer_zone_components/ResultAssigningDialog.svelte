<script lang="ts">
    import BaseDialog from "../../../../../../../../components/BaseDialog.svelte";
    import { StringUtils } from "../../../../../../../../ts/utils/StringUtils";

    export async function open(chosenResults: { [key: string]: string }) {
        dialogElement.open();
        chosenResultsRef = chosenResults;
        isResultCreationState = false;
        resultsRadioInputName = "results-" + StringUtils.randomString(4);
        await fetchResults();
    }
    export let testId: string;
    let dialogElement: BaseDialog;
    let isResultCreationState: boolean = false;
    let chosenResultsRef: { [key: string]: string } = {};
    let allResults: { [key: string]: string } = {};
    let resultsRadioInputName: string;
    async function fetchResults() {
        const url =
            "/api/testCreation/general/getResultsIdNameDictionary/" + testId;
        const response = await fetch(url);
        console.log(response);
    }
    function getAllResultWithoutChosen(): { [key: string]: string } {
        return Object.keys(allResults)
            .filter((key) => !chosenResultsRef[key])
            .reduce((res: { [key: string]: string }, key: string) => {
                res[key] = allResults[key];
                return res;
            }, {});
    }
</script>

<BaseDialog dialogId="result-assigning-dialog" bind:this={dialogElement}>
    {#if isResultCreationState}
        <div class="result-creation-state">
            <h2 class="result-creation-title">Result creation</h2>
            <input type="text" placeholder="Enter result name..." />
        </div>
    {:else}
        <div class="result-assigning-state">
            <h2 class="result-assigning-title">
                Choose a result from the following
            </h2>
            <div class="resuls-options">
                {#each Object.entries(getAllResultWithoutChosen()) as [key, value]}
                    <input
                        type="radio"
                        name={resultsRadioInputName}
                        id={resultsRadioInputName + key}
                    />
                    <label
                        class="result-option"
                        for={resultsRadioInputName + key}
                    >
                        {value}
                    </label>
                {/each}
            </div>
        </div>
    {/if}
</BaseDialog>

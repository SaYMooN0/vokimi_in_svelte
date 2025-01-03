<script lang="ts">
    import type { GeneralTestTakenResultVm } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakenReceivedResultData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let receivedResultId: string;
    export let allResults: GeneralTestTakenResultVm[];

</script>

<h2 class="all-results-label">All possible results:</h2>
{#each allResults.sort((a, b) => b.receivingPercentage - a.receivingPercentage) as res}
    <div class="result" class:received-res={res.id == receivedResultId}>
        <img
            class="res-image unselectable"
            src={ImgUtils.imgUrl(res.image)}
            alt="results"
        />
        <div class="res-name-with-percentage">
            <span class="res-name">{res.name}</span>
            <div class="res-percentage unselectable">
                <span>{res.receivingPercentage}%</span>
                <div class="percentage-bar">
                    <div
                        class="percentage-fill"
                        style="width: {res.receivingPercentage}%;"
                    ></div>
                </div>
            </div>
        </div>
    </div>
{/each}

<style>
    .all-results-label {
        font-size: 20px;
        color: var(--text);
        margin: 0;
    }

    .result {
        margin: 4px 0;
        display: grid;
        height: 100px;
        width: 420px;
        grid-template-columns: 80px 1fr;
        align-items: center;
        padding: 4px 8px;
        box-sizing: border-box;
        border-radius: 8px;
        border: 3px solid var(--back-secondary);
    }
    .res-image {
        width: 100%;
        aspect-ratio: 1/1;
        border-radius: 8px;
        object-fit: cover;
    }
    .res-name-with-percentage {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        padding-left: 12px;
        gap: 8px;
    }
    .res-name {
        font-size: 1.2rem;
        font-weight: bold;
        color: var(--text);
        margin: 0;
    }
    .res-percentage {
        display: flex;
        align-items: center;
        gap: 4px;
    }
    .res-percentage span {
        font-size: 0.9rem;
        color: var(--text-faded);
        width: 60px;
        text-align: right;
    }
    .percentage-bar {
        width: 100%;
        height: 16px;
        background-color: var(--back-secondary);
        border-radius: 8px;
        overflow: hidden;
    }
    .percentage-fill {
        height: 100%;
        background-color: var(--primary);
        border-radius: 8px 0 0 8px;
    }
    .received-res {
        border-color: var(--primary);
        transform: scale(1.04);
    }
</style>

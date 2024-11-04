<script lang="ts">
    import type { GeneralTestTakenResultVm } from "../../../../ts/page_classes/test_taking_page/general_test/GeneralTestTakenReceivedResultData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";


    export let receivedResultId: string;
    export let allResults: GeneralTestTakenResultVm[];
    function getResultImage(res: GeneralTestTakenResultVm) {
        const resKey = StringUtils.isNullOrWhiteSpace(res.Image)
            ? "common/result.webp"
            : (res.Image ?? "");
        return ImgUtils.imgUrl(resKey);
    }
</script>

<div class="all-results-container">
    {#each allResults as res}
        <div class="result" class:received-res={res.id == receivedResultId}>
            <img class="res-image" src={getResultImage(res)} alt="results" />
            <div class="res-name-with-percentage">
                <label class="res-name">{res.name}</label>
                <div class="res-percentage">
                    <label>{res.receivingPercentage}</label>
                    <div style="width: {res.receivingPercentage}%"></div>
                </div>
            </div>
        </div>
    {/each}
</div>

<style>
    .all-results-container {
        display: flex;
        flex-direction: column;
        align-items: centers;
    }
    .res-image {
        width: 100%;
        aspect-ratio: 1/1;
        object-fit: fill;
    }
    .result {
        display: grid;
        height: 80px;
        grid-template-columns: 80px 240px;
    }
    .res-name-with-percentage {
        display: grid;
        grid-template-rows: 2fr 1fr;
    }
    .res-name {
        color: var(--text);
    }
    .res-percentage {
        display: grid;
        grid-template-columns: 40px 1fr;
    }
    .res-percentage label {
        color: var(--text-faded);
    }
    .res-percentage div {
        height: 16px;
        background-color: var(--primary);
    }
</style>

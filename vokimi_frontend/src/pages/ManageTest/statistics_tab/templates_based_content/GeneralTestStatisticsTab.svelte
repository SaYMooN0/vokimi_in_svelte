<script lang="ts">
    import type { GeneralTestStatisticsData } from "../../../../ts/page_classes/manage_test_page/statistics/templates/GeneralTestStatisticsData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import StatisticsTabSectionHeader from "../sections_shared_components/StatisticsTabSectionHeader.svelte";
    import DiscussionsStatisticsTabSection from "../tab_sections/DiscussionsStatisticsTabSection.svelte";
    import RatingsStatisticsTabSection from "../tab_sections/RatingsStatisticsTabSection.svelte";
    import TestTakingsStatisticsTabSection from "../tab_sections/TestTakingsStatisticsTabSection.svelte";

    export let tabData: GeneralTestStatisticsData;

    const resultsWithPercentage = tabData.results.map((result) => ({
        ...result,
        percentage: parseFloat(
            (
                (result.testTakenRecordsCount / tabData.testTakenRecords.all) *
                100
            ).toFixed(2),
        ),
    }));
    const highestPercentage = Math.max(
        ...resultsWithPercentage.map((result) => result.percentage),
    );
    const mostPopularResults = resultsWithPercentage.filter(
        (result) =>
            result.percentage === highestPercentage &&
            result.testTakenRecordsCount > 0,
    );

    const otherResults = resultsWithPercentage
        .filter((result) => !mostPopularResults.includes(result))
        .sort((a, b) => b.testTakenRecordsCount - a.testTakenRecordsCount);
</script>

<TestTakingsStatisticsTabSection sectionData={tabData.testTakenRecords} />
<RatingsStatisticsTabSection sectionData={tabData.ratings} />
<DiscussionsStatisticsTabSection sectionData={tabData.discussions} />

<StatisticsTabSectionHeader text="Results of the test" />

{#if mostPopularResults.length > 0}
    <h3 class="results-h">
        Most Popular {mostPopularResults.length == 1 ? "Result" : "Results"}
    </h3>
    <div class="most-popular-results">
        {#each mostPopularResults as result}
            <div class="most-popular-result-item">
                <img
                    src={ImgUtils.imgUrl(result.resultImage)}
                    alt="Result"
                    class="unselectable"
                />
                <h4 class="result-name">{result.resultName}</h4>
                <p>
                    This result was received by {result.percentage}% of the test
                    takers
                </p>
                <span class="certain-amount">
                    Result was received {result.testTakenRecordsCount} times
                </span>
            </div>
        {/each}
    </div>
{/if}
{#if otherResults.length > 0}
    <h3 class="results-h">
        {mostPopularResults.length === 0 ? "Results" : " Other Results"}
    </h3>
    <div class="other-results-list">
        {#each otherResults as result}
            <div class="result-item">
                <img
                    class="unselectable"
                    src={ImgUtils.imgUrl(result.resultImage)}
                    alt="results"
                />
                <div class="res-name-with-percentage">
                    <span class="res-name">{result.resultName}</span>
                    <div class="res-percentage unselectable">
                        <span>{result.percentage}%</span>
                        <div class="percentage-bar">
                            <div
                                class="percentage-fill"
                                style="width: {result.percentage}%;"
                            ></div>
                        </div>
                    </div>
                    <span class="certain-amount">
                        Result was received {result.testTakenRecordsCount} times
                    </span>
                </div>
            </div>
        {/each}
    </div>
{/if}

<style>
    .results-h {
        margin: 8px 0 0 0;
        width: 100%;
        text-align: center;
        color: var(--text-faded);
    }
    .most-popular-results {
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: center;
        flex-wrap: wrap;
        gap: 16px;
    }
    .most-popular-result-item {
        margin-top: 12px;
        display: grid;
        grid-template-rows: 120px auto auto auto;
        align-items: center;
        justify-items: center;
        border-radius: 8px;
        padding: 8px;
        max-width: 320px;
        box-sizing: border-box;
        box-shadow: rgba(0, 0, 0, 0.12) 0px 3px 6px;
    }
    .most-popular-result-item img {
        height: 100%;
        object-fit: contain;
        max-width: inherit;
        border-radius: 4px;
    }
    .most-popular-result-item .result-name {
        margin: 12px 0 0 0;
        display: block;
        word-break: break-all;
        max-width: 90%;
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
    }
    .most-popular-result-item p {
        text-align: center;
    }
    .other-results-list {
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: center;
    }
    .result-item {
        margin-top: 12px;
        display: grid;
        grid-template-columns: 80px 1fr;
        align-items: center;
        padding: 4px 8px;
        box-sizing: border-box;
        border-radius: 8px;
        border: 3px solid var(--back-secondary);
        width: 420px;
    }

    .result-item img {
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
        gap: 8px;
    }

    .res-percentage span {
        font-size: 16px;
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

    .certain-amount {
        font-size: 14px;
        color: var(--text-faded);
        text-align: center;
        margin-top: 2px;
    }
</style>

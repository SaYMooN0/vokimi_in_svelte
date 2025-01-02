<script lang="ts">
    import type { GeneralTestStatisticsData } from "../../../../ts/page_classes/manage_test_page/statistics/templates/GeneralTestStatisticsData";
    import StatisticsTabSectionHeader from "../sections_shared_components/StatisticsTabSectionHeader.svelte";
    import DiscussionsStatisticsTabSection from "../tab_sections/DiscussionsStatisticsTabSection.svelte";
    import RatingsStatisticsTabSection from "../tab_sections/RatingsStatisticsTabSection.svelte";
    import TestTakingsStatisticsTabSection from "../tab_sections/TestTakingsStatisticsTabSection.svelte";

    export let tabData: GeneralTestStatisticsData;

    function calculatePercentage(
        testTakenRecordsCount: number,
        totalTestTakenRecordsCount: number,
    ): number {
        return (testTakenRecordsCount / totalTestTakenRecordsCount) * 100;
    }

    const totalTestTakenRecordsCount = tabData.results.reduce(
        (total, record) => total + record.testTakenRecordsCount,
        0,
    );

    const resultsWithPercentage = tabData.results.map((result) => ({
        ...result,
        percentage: calculatePercentage(
            result.testTakenRecordsCount,
            totalTestTakenRecordsCount,
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
        .filter((result) => result.percentage !== highestPercentage)
        .sort((a, b) => b.testTakenRecordsCount - a.testTakenRecordsCount);
</script>

<TestTakingsStatisticsTabSection sectionData={tabData.testTakenRecords} />
<RatingsStatisticsTabSection sectionData={tabData.ratings} />
<DiscussionsStatisticsTabSection sectionData={tabData.discussions} />

<StatisticsTabSectionHeader text="Results of the test" />

<div class="results-container">
    {#if mostPopularResults.length > 0}
        <h3>
            Most Popular {mostPopularResults.length == 1 ? "Result" : "Results"}
        </h3>
        {#each mostPopularResults as result}
            <div class="most-popular-results">
                <img
                    src={result.resultImage}
                    alt="Result Image"
                    class="result-image"
                />
                <div class="result-details">
                    <h4>{result.resultName}</h4>
                    <p>
                        Test Taken Records: {result.testTakenRecordsCount}
                    </p>
                    <p>Percentage: {result.percentage.toFixed(2)}%</p>
                </div>
            </div>
        {/each}
    {/if}

    <h3>All Other Results</h3>
    <div class="result-list">
        {#each otherResults as result}
            <div class="result-item">
                <img
                    src={result.resultImage}
                    alt="Result Image"
                    class="result-image"
                />
                <div class="result-details">
                    <h4>{result.resultName}</h4>
                    <p>Test Taken Records: {result.testTakenRecordsCount}</p>
                    <p>Percentage: {result.percentage.toFixed(2)}%</p>
                </div>
            </div>
        {/each}
    </div>
</div>

<style>
    .results-container {
        padding: 20px;
        border-radius: 8px;
    }

    .result h3 {
        font-size: 24px;
        margin-top: 20px;
    }

    .result-item {
        display: flex;
        align-items: center;
        margin-bottom: 15px;
        padding: 10px;
        border-radius: 8px;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    }

    .result-image {
        width: 50px;
        height: 50px;
        object-fit: cover;
        margin-right: 15px;
        border-radius: 50%;
    }

    .result-details h4 {
        font-size: 18px;
        margin: 0;
    }

    .result-details p {
        font-size: 14px;
    }

    .result-list {
        display: flex;
        flex-direction: column;
    }
</style>

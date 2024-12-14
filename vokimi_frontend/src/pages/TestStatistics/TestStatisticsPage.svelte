<script lang="ts">
    import type { ITestStatisticsData } from "../../ts/page_classes/test_statistics/ITestStatisticsData";

    export let testId:string;
async function  fetchTestStatistics():ITestStatisticsData|Err {
    const response=await fetch();
    const data=await response.json()

}
</script>


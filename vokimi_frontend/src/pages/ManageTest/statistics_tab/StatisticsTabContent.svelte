<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import TabDataFetchingErrDiv from "../tabs_shared/TabDataFetchingErrDiv.svelte";

    export let testId: string;
    let tabData: StatisticsTabData | null;
    let tabFetchErr: string = "";
    interface StatisticsTabData {}
    async function getTabData(forcedFetch: boolean = false) {
        tabFetchErr = "";
        if (tabData !== null && !forcedFetch) {
            return;
        }
        const fetchRes = await fetchTabData();
        if (fetchRes instanceof Err) {
            tabFetchErr = fetchRes.toString();
        } else {
            tabData = fetchRes;
        }
    }
    async function fetchTabData(): Promise<StatisticsTabData | Err> {
        return new Err("Not implemented");
    }
</script>

{#if !StringUtils.isNullOrWhiteSpace(tabFetchErr)}
    <TabDataFetchingErrDiv
        err={tabFetchErr}
        tryAgainAction={() => getTabData(true)}
    />
{:else if tabData === null}
    <TabDataFetchingErrDiv
        err="Unable to fetch data tab"
        tryAgainAction={() => getTabData(true)}
    />
{:else}
    <div>Statistics content</div>
{/if}

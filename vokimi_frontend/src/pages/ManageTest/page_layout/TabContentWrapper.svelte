<script lang="ts" generics="T">
    import { Err } from "../../../ts/Err";
    import TabDataFetchingErrDiv from "../tabs_shared/TabDataFetchingErrDiv.svelte";

    export let fetchTabData: () => Promise<T | Err>;
    export let isActive: boolean;
    let tabDataSetRes: T | Err;
    async function setTabData(forcedFetch: boolean = false): Promise<void> {
        if (tabDataSetRes instanceof Err && !forcedFetch) {
            return;
        }
        const fetchRes = await fetchTabData();
        if (fetchRes instanceof Err) {
            tabDataSetRes = new Err(fetchRes.toString());
        } else {
            tabDataSetRes = fetchRes;
        }

        console.log(`Fetched tab data ${fetchTabData}`, tabDataSetRes);
    }
</script>

<div class="tab-content" class:activeTabContent={isActive}>
    {#if !isActive}
        <p>Tab not active</p>
    {:else if tabDataSetRes === undefined}
        {setTabData()}
    {:else if tabDataSetRes instanceof Err}
        <TabDataFetchingErrDiv
            err={tabDataSetRes.toString()}
            tryAgainAction={() => setTabData(true)}
        />
    {:else}
        <slot {tabDataSetRes} updateTabData={() => setTabData(true)} />
    {/if}
</div>

<style>
    .tab-content {
        display: none;
    }
    .tab-content.activeTabContent {
        display: block;
        box-sizing: border-box;
        padding: 4px 12px;
    }
</style>

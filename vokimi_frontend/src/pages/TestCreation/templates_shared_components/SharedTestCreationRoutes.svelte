<script lang="ts">
    import DraftTestMainInfoView from "./DraftTestMainInfoView.svelte";
    import DraftTestConcusionView from "./DraftTestConcusionView.svelte";
    import DraftTestStylesView from "./DraftTestStylesView.svelte";
    import { Route } from "svelte-routing";

    export let mainInfoPath: string;
    export let viewConclusionPath: string;
    export let viewStylesPath: string;
</script>

<Route path={mainInfoPath} component={DraftTestMainInfoView} />
<Route path={viewConclusionPath} component={DraftTestConcusionView} />
<Route path={viewStylesPath} component={DraftTestStylesView} />

<script lang="ts">
    import DraftTestConcusionView from "./DraftTestConcusionView.svelte";
    import DraftTestStylesView from "./DraftTestStylesView.svelte";
    import { Route } from "svelte-routing";

    export let viewStylesPath: string;
    export let viewConclusionPath: string;
</script>

<Route path={viewStylesPath} component={DraftTestStylesView} />
<Route path={viewConclusionPath} component={DraftTestConcusionView} />

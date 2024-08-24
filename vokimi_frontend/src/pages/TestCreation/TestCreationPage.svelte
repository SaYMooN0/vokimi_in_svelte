<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import { onMount } from "svelte";
    import GeneralTestOverview from "./general_test_creation_components/GeneralTestOverview.svelte";
    import GeneralTestQuestionEditing from "./general_test_creation_components/GeneralTestQuestionEditing.svelte";
    import DraftTestConcusionEditing from "./templates_shared_components/DraftTestConcusionEditing.svelte";
    import DraftTestStylesEditing from "./templates_shared_components/DraftTestStylesEditing.svelte";
    import { TestTemplate } from "../../ts/enums/TestTemplate";

    export let params;

    let template: TestTemplate;

    onMount(async () => {
        template = TestTemplate.General;
        //fetch from server
    });
</script>

<Router>

    <Route path="gen-overview" component={GeneralTestOverview} />
    <Route path="gen-question-editing" component={GeneralTestQuestionEditing} />
    <Route path="styles-editing" component={DraftTestStylesEditing} />
    <Route path="conclusion-editing" component={DraftTestConcusionEditing} />
</Router>

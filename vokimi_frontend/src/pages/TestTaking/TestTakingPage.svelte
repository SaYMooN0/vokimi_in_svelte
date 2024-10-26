<script lang="ts">
    import TestAccessPageContainer from "../../components/test_access_page_container/TestAccessPageContainer.svelte";
    export let testId: string;
   
</script>

<TestAccessPageContainer {testId}>
    Test taking page
</TestAccessPageContainer>
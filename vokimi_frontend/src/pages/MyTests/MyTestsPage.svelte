<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import LoadingMessage from "../../components/shared/LoadingMessage.svelte";
    import MyTestsTabsContainer from "./my_tests_page_components/MyTestsTabsContainer.svelte";
    
</script>

<AuthorizeView>
    <div slot="loading">
        <LoadingMessage />
    </div>
    <div slot="authenticated">
        <MyTestsTabsContainer />
    </div>
    <div slot="unauthenticated">
        <p>You have to be authenticated to access this page</p>
    </div>
</AuthorizeView>

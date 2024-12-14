<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";

    let fetchingErr: string;
    export async function open(publishedTestId: string) {
        const response = await fetch(
            `/api/postsCreation/getTestPublishedPostCreationData/${publishedTestId}`,
        );
        if (response.ok) {
            return "Everything went fine!";
        } else if (response.status === 400) {
            return (fetchingErr = await getErrorFromResponse(response));
        } else {
            return (fetchingErr = "An unknown error occurred.");
        }
    }
</script>

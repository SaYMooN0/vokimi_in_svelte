<script lang="ts">
    export let setErrorMessage: (message: string) => void;
    export let changeStateToResultAssigning: () => Promise<void>;
    export let testId: string;
    let resultName: string = "";

    export async function onCreateButtonClick() {
        if (resultName === "") {
            setErrorMessage("Result name cannot be empty");
            return;
        }
        const data = { resultName, testId };
        const response = await fetch(
            "/api/testCreation/general/createNewResult",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            },
        );

        if (response.ok) {
            await changeStateToResultAssigning();
        } else if (response.status === 400) {
            const errorResponse = await response.json();
            setErrorMessage(
                errorResponse.error || "An unknown error occurred.",
            );
        } else {
            setErrorMessage("An unknown error occurred.");
        }
    }
</script>

<div class="result-creation-state">
    <p class="result-creation-title">Result creation:</p>
    <input
        bind:value={resultName}
        type="text"
        class="result-name-input"
        placeholder="Enter result name..."
    />
</div>

<style>
    .result-creation-state {
        width: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        height: min-content;
    }
    .result-creation-title {
        margin: 0;
        font-size: 24px;
        font-weight: 500;
        color: var(--text-faded);
    }
    .result-name-input {
        margin-top: 40px;
        width: 80%;
        padding: 8px 20px;
        background-color: var(--back-secondary);
        color: var(--text);
        border: 3px solid transparent;
        border-radius: 60px;
        font-size: 24px;
        outline: none;
    }

    .result-name-input:focus {
        border-color: var(--primary);
    }
</style>

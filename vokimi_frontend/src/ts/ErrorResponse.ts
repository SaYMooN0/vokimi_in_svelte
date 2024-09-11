export interface ErrorResponse {
    error: string;
}
export async function getErrorFromResponse(response: Response): Promise<string> {
    const data: ErrorResponse = await response.json();
    return data.error;
}


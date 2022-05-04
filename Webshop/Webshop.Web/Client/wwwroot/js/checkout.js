// This is your test publishable API key.
const stripe = Stripe("pk_test_51KvQWBDwqMZnbyqHU5t2SlfEaCrsn5VZXlXSRpFNVRb3xiqhcTxZWwZL1nLZYhkjfce7LUXQcMSRkP41HAGc23Fw00EI1ojtNS");

// The items the customer wants to buy
const items = [{ id: "xl-tshirt" }];

// Fetches a payment intent and captures the client secret
async function initialize(customerId, userId) {
	const response =  await fetch("api/create-payment-intent", {
		method: "POST",
		headers: { "Content-Type": "application/json" },
		body: JSON.stringify({ customerId, userId }),
	});
	const { clientSecret } = await response.json();
	const appearance = {
		theme: 'stripe',
	};
	var elements = stripe.elements({ appearance, clientSecret });
	var card = elements.create('card');
	card.mount('#card-element');
	card.on('change', function (event) {
		var displayError = document.getElementById('card-errors');
		if (event.error) {
			displayError.textContent = event.error.message;
		} else {
			displayError.textContent = '';
		}
	});
	var form = document.getElementById('payment-form');
	form.addEventListener('submit', async function (ev) {
		ev.preventDefault();
		// If the client secret was rendered server-side as a data-secret attribute
		// on the <form> element, you can retrieve it here by calling `form.dataset.secret`
		stripe.confirmCardPayment(clientSecret, {
			payment_method: {
				type: 'card',
				card: card
			}
		}).then(async function (result) {
			if (result.error) {
				// Show error to your customer (for example, insufficient funds)
				console.log(result.error.message);
			} else {
				// The payment has been processed!
				const orderResponse = await fetch(`api/order/json?userid=${userId}&customerid=${customerId}`, {
					method: "POST",
					headers: { "Content-Type": "application/json" },
				});
				const { orderId } = await orderResponse.json();

				if (result.paymentIntent.status === 'succeeded') {
					window.location.href = `/order/${orderId}`;
				}
			}
		});
	});
}
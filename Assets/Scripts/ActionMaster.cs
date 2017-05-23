using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ActionMaster {
	public enum Action {Tidy, Reset, EndTurn, EndGame, Undefined};
	
	public static Action NextAction (List<int> rolls) {
		Action nextAction = Action.Undefined;

	    var localRolls = new List<int>(rolls);        

		for (int i = 0; i < localRolls.Count; i++) { // Step through localRolls
			
			if (i == 20) {
				nextAction = Action.EndGame;
			} else if ( i >= 18 && localRolls[i] == 10 ){ // Handle last-frame special cases
				nextAction = Action.Reset;
			} else if ( i == 19 ) {
				if (localRolls[18]==10 && localRolls[19]==0) {
					nextAction = Action.Tidy;
				} else if (localRolls[18] + localRolls[19] == 10) {
					nextAction = Action.Reset;
				} else if (localRolls [18] + localRolls[19] >= 10) {  // Roll 21 awarded
					nextAction = Action.Tidy;
				} else {
					nextAction = Action.EndGame;
				}
			} else if (i % 2 == 0) { // First bowl of frame
				if (localRolls[i] == 10) {
					localRolls.Insert (i + 1, 0); // Insert virtual 0 after strike
					nextAction = Action.EndTurn;
				} else {
					nextAction = Action.Tidy;
				}
			} else { // Second bowl of frame
				nextAction = Action.EndTurn;
			}
		}
		
		return nextAction;
	}
}